using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyOil.Models;

namespace MyOil.Controllers
{
    public class AccountController : Controller
    {
        //
        OilEntities db = new OilEntities();

        // GET: /Account/LogOn
        public ActionResult LogOn()
        {
            return View();
        }


        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (ValidateUser(model.UserName, model.Password))
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "提供的用户名或密码不正确。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // 尝试注册用户

                if (model.Password == model.ConfirmPassword && 0 == db.user_enc.Where(c => c.Name == model.UserName).Count())
                {
                    user user = new user { ID = 0, Name = model.UserName, PWD = model.Password, Role = "1" };
                    user.ID = db.user.Max(c => c.ID) + 1;
                    db.user.Add(user);

                    /* 用触发器实现
                    user_enc user_en = new user_enc { ID = user.ID, Name = model.UserName, PWD = model.Password, Role = "1" };
                    if (EncryptPwd(model.Password) != "0")
                    {
                        user_en.PWD = EncryptPwd(model.Password);
                    }
                    db.user_enc.AddObject(user_en);
                     * */

                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "用户名已存在。请输入不同的用户名");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }
    


        public bool ValidateUser(string name, string pwd)
        {
            string mstr = "";
            mstr = EncryptPwd(pwd);
            try
            {

                db.user_enc.First(c => (c.Name == name && c.PWD == mstr));
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public string EncryptPwd(string str)
        {
            string encryptStr = "";
            try
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] hashedDataBytes;
                hashedDataBytes = md5Hasher.ComputeHash(System.Text.Encoding.GetEncoding("gb2312").GetBytes(str));
                System.Text.StringBuilder tmp = new System.Text.StringBuilder();
                foreach (byte i in hashedDataBytes)
                {
                    tmp.Append(i.ToString("x2"));
                }
                encryptStr = tmp.ToString().Substring(0,32);
            }
            catch (ArgumentException ex) {  throw ex; }
            catch (System.Security.Cryptography.CryptographicException ex) {  throw ex; }
            catch (Exception ex) { throw ex; }

            return encryptStr;

        }

    }
}
