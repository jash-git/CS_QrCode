using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//---
//QrCode
using ZXing;
using ZXing.QrCode;
using ZXing.Rendering;
//---QrCode

//---
//參考資料
//https://www.cnblogs.com/chenwolong/p/erweima.html
//https://dangerlover9403.pixnet.net/blog/post/199434147-%5B%E6%95%99%E5%AD%B8%5D-decode-encode-qrcode-(c%23-version)
//https://dotblogs.com.tw/neil_coding/2019/10/25/qrcode
//---參考資料

namespace CS_QrCode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Bitmap GetQRCodeByZXingNet(String strMessage, Int32 width, Int32 height)
        {
            String imagePath = System.Windows.Forms.Application.StartupPath + "\\ICON.png";
            Bitmap result = null;
            try
            {
                BarcodeWriter barCodeWriter = new BarcodeWriter();
                barCodeWriter.Format = BarcodeFormat.QR_CODE; //barcode格式
                barCodeWriter.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");  //編碼字元utf-8
                barCodeWriter.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H); //錯誤校正等級
                barCodeWriter.Options.Height = height; //高度
                barCodeWriter.Options.Width = width; //寬度
                barCodeWriter.Options.Margin = 0; //外邊距
                ZXing.Common.BitMatrix bm = barCodeWriter.Encode(strMessage); //將訊息寫入
                result = barCodeWriter.Write(bm);

                Bitmap overlay = new Bitmap(imagePath); //載入圖片

                int deltaHeigth = result.Height - overlay.Height; //圖片y
                int deltaWidth = result.Width - overlay.Width; //圖片x

                Graphics g = Graphics.FromImage(result); //圖型
                g.DrawImage(overlay, new Point(deltaWidth / 2, deltaHeigth / 2)); //畫出圖片
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return result;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //---
            //單純QR
                Bitmap bitmap = null;
                string strQrCodeContent = "http://www.syris.com/index.php";
                ZXing.BarcodeWriter writer = new ZXing.BarcodeWriter
                {
                    Format = ZXing.BarcodeFormat.QR_CODE,
                    Options = new ZXing.QrCode.QrCodeEncodingOptions
                    {
                        //Create Photo 
                        Height = 200,
                        Width = 200,
                        CharacterSet = "UTF-8",
                        //錯誤修正容量
                        //L水平    7%的字碼可被修正
                        //M水平    15%的字碼可被修正
                        //Q水平    25%的字碼可被修正
                        //H水平    30%的字碼可被修正
                        ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.H
                    }

                };
                    
                bitmap = writer.Write(strQrCodeContent);//Create Qr-code , use input string

                //---
                //Storage bitmpa
                string strDir;
                strDir = System.Windows.Forms.Application.StartupPath;
                strDir += "\\temp.png";
                bitmap.Save(strDir, System.Drawing.Imaging.ImageFormat.Png);
                //---Storage bitmpa
            //---單純QR

            //---
            //插入LOGO
            strDir = System.Windows.Forms.Application.StartupPath;
            strDir += "\\temp01.png";
            GetQRCodeByZXingNet("http://www.syris.com/index.php", 200, 200).Save(strDir, System.Drawing.Imaging.ImageFormat.Png);
            //---插入LOGO
        }
    }
}
