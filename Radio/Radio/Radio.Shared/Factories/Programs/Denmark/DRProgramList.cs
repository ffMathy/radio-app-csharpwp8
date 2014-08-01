using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using HtmlAgilityPack;
using Radio.Models;

namespace RadioV2.Controllers.Programs.Denmark
{
    class DRProgramList : ProgramList
    {

        private readonly string _radioCode;

        public DRProgramList(string radioCode)
        {
            _radioCode = radioCode;
            RefreshPrograms();
        }

        protected override sealed async Task RefreshPrograms()
        {
            if (IgnorePrograms) return;

            try
            {

                //TODO: store on isolated storage.

                var client = new WebClient();
                var url = string.Format("http://www.dr.dk/{0}/sendeplan", _radioCode);

                var isDone = false;
                var html = string.Empty;

                client.DownloadStringCompleted += delegate(object sender, DownloadStringCompletedEventArgs args)
                {
                    try
                    {
                        html = args.Result;
                    }
                    catch
                    {
                        html = string.Empty;
                    }
                    isDone = true;
                };

                client.DownloadStringAsync(new Uri(url));

                while (!isDone)
                {
                    await Task.Delay(10);
                }

                if (!string.IsNullOrEmpty(html))
                {

                    var document = new HtmlDocument();
                    document.LoadHtml(html);

                    var root = document.DocumentNode;
                    if (root != null)
                    {
                        var itemNodes = root.SelectNodes("//article[@class='item']");
                        if (itemNodes != null)
                        {
                            foreach (var node in itemNodes)
                            {
                                var timeNode = node.SelectSingleNode(".//time");
                                var titleNode = node.SelectSingleNode(".//*[@class='title']");

                                var program = new Program(this);
                                program.Time = DateTime.ParseExact(timeNode.InnerText.Trim(), "HH:mm",
                                    new CultureInfo("da-DK"));
                                program.Title = HtmlEntity.DeEntitize(titleNode.InnerText.Trim());

                                Debug.WriteLine("Program added to " + _radioCode + ": " + program.Title + " (" +
                                                program.Time + ")");
                                Programs.AddLast(program);
                            }

                            RefreshProperties();

                            OnPropertyChanged("CurrentProgram");
                            OnPropertyChanged("RemainingPrograms");
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("An exception occured when scraping: " + ex);
                if (Debugger.IsAttached) throw;
            }

        }
    }
}
