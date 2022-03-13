using MyNotes.Dao;
using MyNotes.Entity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyNotes.Controllers
{
    public class AdminController : Controller
    {
        CommonDao dao = new CommonDao();
        // GET: Admin
        public ActionResult Index(string sa, int page = 1, int pageSize = 20)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            ViewBag.Search = sa;
            var model = dao.ListAllPaging(sa, page, pageSize, true);
            return View(model);
        }

        public ActionResult Edit(Guid? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (id == null)
                return RedirectToAction("Index");
            var model = dao.GetNote(id.Value);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(tbl_Notes model)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (dao.EditNote(model))
            {
                return RedirectToAction("Index");
            }
            else
                return View(model);
        }

        public ActionResult Delete(Guid? id)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (id == null)
                return RedirectToAction("Index");
            dao.DeleteNote(id.Value);
            return RedirectToAction("Index");
        }

        public ActionResult HSKey(string hid)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (!string.IsNullOrEmpty(hid))
            {
                ViewBag.ID = hid;
                ViewBag.Key = GenerateHSMaster(hid);
            }
            return View();
        }

        private string GenerateHSMaster(string productID)
        {
            var key = OmegaLib.Encryption.StringCipher.Encrypt(productID , "keyhsmaster.blogspot.com");
            return key;
        }

        public ActionResult Hanbot(string key)
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("DangNhap", "Login");
            if (!string.IsNullOrEmpty(key))
            {
                key = key + "|" + method_1(10) + "|" + smethod_1();
                var temp = smethod_2(key);
                NameValueCollection nameValueCollection_ = new NameValueCollection
                        {
                            {
                                "key",
                                temp
                            }
                        };
                var res = smethod_0(method_1(nameValueCollection_));
                ViewBag.ID = key;
                ViewBag.Key = res;
            }
            return View();
        }

        private string method_1(int int_0)
        {
            Random random_0 = new Random();
            string text = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < int_0; i++)
            {
                char value = text[random_0.Next(0, text.Length)];
                stringBuilder.Append(value);
            }
            return stringBuilder.ToString();
        }
        private string smethod_2(string string_2)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Padding = PaddingMode.Zeros;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 256;
            byte[] bytes = Encoding.ASCII.GetBytes("157rwf897222bbbtrm8814z5qq449157");
            byte[] bytes2 = Encoding.ASCII.GetBytes("157952hheeyy666cs99hjv887mxx7157");
            ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes, bytes2);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            byte[] bytes3 = Encoding.ASCII.GetBytes(string_2);
            cryptoStream.Write(bytes3, 0, bytes3.Length);
            cryptoStream.FlushFinalBlock();
            return Convert.ToBase64String(memoryStream.ToArray());
        }

        private string method_1(NameValueCollection nameValueCollection_0)
        {
            string address = "http://auth.thuetoolgame.com/" + "hanauth/login.php";
            string result;
            try
            {
                using (WebClient webClient_0 = new WebClient())
                {
                    result = Encoding.UTF8.GetString(webClient_0.UploadValues(address, nameValueCollection_0));
                }
            }
            catch (Exception ex)
            {
                result = string.Empty;
            }
            return result;
        }

        private string smethod_0(string string_2)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Padding = PaddingMode.Zeros;
            rijndaelManaged.Mode = CipherMode.CBC;
            rijndaelManaged.KeySize = 256;
            rijndaelManaged.BlockSize = 256;
            byte[] bytes = Encoding.ASCII.GetBytes("157rwf897222bbbtrm8814z5qq449157");
            byte[] bytes2 = Encoding.ASCII.GetBytes("157952hheeyy666cs99hjv887mxx7157");
            ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes, bytes2);
            byte[] array = Convert.FromBase64String(string_2);
            byte[] array2 = new byte[array.Length];
            new CryptoStream(new MemoryStream(array), transform, CryptoStreamMode.Read).Read(array2, 0, array2.Length);
            return Encoding.ASCII.GetString(array2).Replace("\0", "");
            //return "TKDUNG";
        }

        private string smethod_1()
        {
            byte[] value = new byte[8];
            if (!smethod_2(ref value))
            {
                return "ND";
            }
            return string.Format("{0}{1}", BitConverter.ToUInt32(value, 4).ToString("X8"), BitConverter.ToUInt32(value, 0).ToString("X8"));
        }
        private bool smethod_2(ref byte[] byte_0)
        {
            byte[] array = new byte[]
            {
                85,
                137,
                229,
                87,
                139,
                125,
                16,
                106,
                1,
                88,
                83,
                15,
                162,
                137,
                7,
                137,
                87,
                4,
                91,
                95,
                137,
                236,
                93,
                194,
                16,
                0
            };
            byte[] array2 = new byte[]
            {
                83,
                72,
                199,
                192,
                1,
                0,
                0,
                0,
                15,
                162,
                65,
                137,
                0,
                65,
                137,
                80,
                4,
                91,
                195
            };
            int num;
            if (smethod_3())
            {
                IntPtr intPtr = new IntPtr(array2.Length);
                if (!VirtualProtect(array2, intPtr, 64, out num))
                {
                    Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
                }
                intPtr = new IntPtr(byte_0.Length);
                return CallWindowProcW(array2, IntPtr.Zero, 0, byte_0, intPtr) != IntPtr.Zero;
            }
            IntPtr intPtr2 = new IntPtr(array.Length);
            if (!VirtualProtect(array, intPtr2, 64, out num))
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }
            intPtr2 = new IntPtr(byte_0.Length);
            return CallWindowProcW(array, IntPtr.Zero, 0, byte_0, intPtr2) != IntPtr.Zero;
        }
        private static bool smethod_3()
        {
            return IntPtr.Size == 8;
        }
        [DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr CallWindowProcW([In] byte[] byte_0, IntPtr intptr_0, int int_1, [In] [Out] byte[] byte_1, IntPtr intptr_1);

        // Token: 0x060000A2 RID: 162
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool VirtualProtect([In] byte[] byte_0, IntPtr intptr_0, int int_1, out int int_2);
    }
}