using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestaurantMenu.Restaurant
{
    public partial class BrandingLogo : System.Web.UI.Page
    {
        public string SmallLogoUrl = "~/Template/assets/img/smallLogo_tran.png";
        public string BigLogoUrl = "~/Template/assets/img/logo-2.png";

        protected void Page_Load(object sender, EventArgs e)
        {
            protectPage();

            try
            {
                if (!this.IsPostBack)
                {

                }
                else
                {

                }

                //load restaurant logo urls
                getBrandingLogos(new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void getBrandingLogos(int restId)
        {
            try
            {
                string[] logos = new BLL.Restaurants().GetBrandingLogos(restId);
                if (logos.Length > 0)
                {
                    BigLogoUrl = "~" + logos[0];
                    SmallLogoUrl = "~" + logos[1];
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ImageValid())
                {
                    if (fileUploadBigLogo.HasFile||fileUploadSmallLogo.HasFile)
                    {
                        Objects.Restaurant obj = new Objects.Restaurant();

                        Int32 restaurantId = new BLL.Restaurants().GetRestaurantId(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId]));

                        obj.Id = restaurantId;

                        if (this.fileUploadBigLogo.HasFile)
                        {
                            string bigLogoFileName = Guid.NewGuid().ToString().Replace("-", "");
                            obj.LogoPath = @"/Reso/res/" + restaurantId + "/" + bigLogoFileName + ".png";
                            saveImageBigLogo(bigLogoFileName, restaurantId);
                        }

                        if (this.fileUploadSmallLogo.HasFile)
                        {
                            string smallLogoFileName = Guid.NewGuid().ToString().Replace("-", "");
                            obj.LogoSmallPath = @"/Reso/res/" + restaurantId + "/"+smallLogoFileName+".png";
                            saveImageSmallLogo(smallLogoFileName, restaurantId);
                        }

                        if (new BLL.Restaurants().UpdateBrandingLogos(obj, Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserId])))
                        {
                            Helpers.Alert.Show("Branding Logo has been Saved");
                            getBrandingLogos(restaurantId);
                        }
                        else
                        {
                            Helpers.Alert.Show("Branding Logo wes not Saved");
                        }
                    }
                    else
                    {
                        Response.Redirect("~/");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void saveImageBigLogo(string fileName1,Int32 restaurantId)
        {
            try
            {
                if (this.fileUploadBigLogo.HasFile)
                {
                    Stream strmStream = this.fileUploadBigLogo.PostedFile.InputStream;

                    System.Drawing.Image im = System.Drawing.Image.FromStream(strmStream);

                    string filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath
                        , @"Reso\res\" + restaurantId + "\\");
                    Directory.CreateDirectory(filePath);
                    im.Save(filePath + fileName1 + ".png", ImageFormat.Png);

                    //DELETE IF NOT SHARED LOGO
                    if (!BigLogoUrl.Contains("Template/assets/img"))
                    {
                        if (File.Exists(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @BigLogoUrl.Replace("~/", "").Replace("/", "\\"))))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @BigLogoUrl.Replace("~/", "").Replace("/", "\\")));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void saveImageSmallLogo(string fileName2,Int32 restaurantId)
        {
            try
            {
                if (this.fileUploadSmallLogo.HasFile)
                {
                    Stream strmStream = this.fileUploadSmallLogo.PostedFile.InputStream;

                    System.Drawing.Image im = System.Drawing.Image.FromStream(strmStream);

                    string filePath = Path.Combine(HostingEnvironment.ApplicationPhysicalPath
                        , @"Reso\res\" + restaurantId + "\\");
                    Directory.CreateDirectory(filePath);
                    im.Save(filePath + fileName2 + ".png", ImageFormat.Png);

                    //DELETE IF NOT SHARED LOGO
                    if (!SmallLogoUrl.Contains("Template/assets/img"))
                    {
                        if (File.Exists(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @SmallLogoUrl.Replace("~/","").Replace("/","\\"))))
                        {
                            File.Delete(Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @SmallLogoUrl.Replace("~/", "").Replace("/", "\\")));
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private bool ImageValid()
        {
            try
            {
                string[] validFileTypes = { "jpg", "jpeg", "bmp", "png" };
                string ext;
                
                if (this.fileUploadBigLogo.HasFile)
                {
                    ext = System.IO.Path.GetExtension(this.fileUploadBigLogo.PostedFile.FileName);
                    bool isValidFile = false;

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }

                    if (!isValidFile)
                    {
                        Helpers.Alert.Show("Invalid Image Format of Big Logo Please use .png .jpg .jpeg .bmp");
                        return false;
                    }
                }

                if (this.fileUploadSmallLogo.HasFile)
                {
                    ext = System.IO.Path.GetExtension(this.fileUploadSmallLogo.PostedFile.FileName);
                    bool isValidFile = false;

                    for (int i = 0; i < validFileTypes.Length; i++)
                    {
                        if (ext == "." + validFileTypes[i])
                        {
                            isValidFile = true;
                            break;
                        }
                    }

                    if (!isValidFile)
                    {
                        Helpers.Alert.Show("Invalid Image Format of Small Logo Please use .png .jpg .jpeg .bmp");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void protectPage()
        {
            //session check
            if (Session[new Helpers.SessionNames().SiteUserId] == null || string.IsNullOrEmpty(Session[new Helpers.SessionNames().SiteUserId].ToString()))
            {
                Response.Redirect("~/Public/Signin.aspx");
            }
            else
            {
                if (Session[new Helpers.SessionNames().SiteUserType] != null)
                {
                    if (Session[new Helpers.SessionNames().SiteUserType].ToString() != "2")
                    {
                        redirectUserType(Convert.ToInt32(Session[new Helpers.SessionNames().SiteUserType]));
                    }
                    //user is restaurant and have session
                }
                else
                {
                    Response.Redirect("~/Public/Signin.aspx");
                }
            }
        }
        private void redirectUserType(Int32 _userType)
        {
            if (_userType == 1)
            {
                Response.Redirect("~/Admin/Home.aspx");
            }
            if (_userType == 3)
            {
                Response.Redirect("~/Waiter/Home.aspx");
            }
        }
    }
}