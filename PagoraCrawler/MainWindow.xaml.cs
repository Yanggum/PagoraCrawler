using Abot.Crawler;
using Abot.Poco;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PagoraCrawler
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //            IWebCrawler crawler = new PoliteWebCrawler();

            //옵션과 함께 크롤러 인스턴스 생성할 경우
            var crawlConfig = new CrawlConfiguration();
            crawlConfig.CrawlTimeoutSeconds = 1000;
            crawlConfig.MaxConcurrentThreads = 10;
            crawlConfig.MaxPagesToCrawl = 10;
            crawlConfig.UserAgentString = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:51.0) Gecko/20100101 Firefox/51.0";
            IWebCrawler crawler = new PoliteWebCrawler(crawlConfig);

            crawler.PageCrawlStartingAsync += (s, evt) =>
            {
                Console.WriteLine("Starting : {0}", evt.PageToCrawl);
            };

            crawler.PageCrawlCompleted += (s, evt) =>
            {
                CrawledPage pg = evt.CrawledPage;

                //                label.Content = pg.Content.Text;
                MessageBox.Show(pg.Content.Text);

                string fn = pg.Uri.Segments[pg.Uri.Segments.Length - 1];
                File.WriteAllText(fn, pg.Content.Text);

                //var hdoc = pg.HtmlDocument; //HtmlAgilityPack HtmlDocument

                Console.WriteLine("Completed : {0}", pg.Uri.AbsoluteUri);
            };


            // 크롤 시작
            string siteUrl = textBox.Text; //"http://www.naver.com";
            Uri uri = new Uri(siteUrl);

            var test = crawler.Crawl(uri);
            //            test.CrawlContext.




        }
    }
}
