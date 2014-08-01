namespace Radio.Messages
{
    class GoToPageMessage
    {
        public string PageName { get; set; }

        public GoToPageMessage()
        {

        }

        public GoToPageMessage(string pageName)
        {
            this.PageName = pageName;
        }
    }
}
