using System;
using System.Net;
namespace test
{
    class Program
    {
        static void Main(string[] args)
        {

            string basic = "https://example.com/example/b01/s43/image/jpg/promotion/found_promo_bg_";
            //これは、実在しないURL(仮定のURL)です。
            //どうやら、文字列の合成は、うまくいってるみたいです。
            int un = 1100000;
            string us = ".jpg";
            string file = @"C:temp\";
            //basic=元のURLのベース
            //un=URLの数列
            //us=.jpgを入れる
            //cnt=カウント確認


            while (true)
            {

                un = un + 100;

                string URL = basic + un + us;
                string dls = file + un + us;

                try
                {
                    WebClient client = new WebClient();
                    client.DownloadFile(URL, dls);
                    client.Dispose();
                }
                catch (Exception e)
                {


                    //ここからが、進行中





                    //WebRequestの作成
                    HttpWebRequest webreq =
                        (HttpWebRequest)WebRequest.Create(URL);

                    HttpWebResponse webres = null;
                    try
                    {
                        //サーバーからの応答を受信するためのWebResponseを取得
                        webres = (HttpWebResponse)webreq.GetResponse();

                        //応答したURIを表示する
                        Console.WriteLine(webres.ResponseUri);
                        //応答ステータスコードを表示する
                        Console.WriteLine("{0}:{1}",
                            webres.StatusCode, webres.StatusDescription);
                        
                    }
                    catch (WebException ex)
                    {
                        //HTTPプロトコルエラーかどうか調べる
                        if (ex.Status == WebExceptionStatus.ProtocolError)
                        {
                            //HttpWebResponseを取得
                            HttpWebResponse errres =
                             GetErrres(ex);
                            //応答したURIを表示する
                            Console.WriteLine(errres.ResponseUri);
                            //応答ステータスコードを表示する
                            Console.WriteLine("{0}:{1}",
                                errres.StatusCode, errres.StatusDescription);
                            // 連続呼び出しでエラーになる場合があるのでその対策
                           
                        }
                        else
                            Console.WriteLine(ex.Message);
                       

                    }
                    
                }
            }
        }

        private static HttpWebResponse GetErrres(WebException ex)
        {
            return (HttpWebResponse)ex.Response;
        }
    }
}
