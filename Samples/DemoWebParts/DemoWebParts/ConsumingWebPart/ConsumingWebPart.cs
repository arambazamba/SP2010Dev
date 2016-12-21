using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Integrations;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace DemoWebParts.ConsumingWebPart
{
    [ToolboxItemAttribute(false)]
    public class ConsumingWebPart : WebPart
    {
        IStringContent data;

        [ConnectionConsumer("String Consumer")]
        public void GetConnectionInterface(IStringContent ExchangedData)
        {
            data = ExchangedData;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (data != null && data.ToString() != string.Empty)
            {
                writer.Write(data.StringValue);

            }
            else
            {
                writer.Write("No Value passed");
            }
        }
    }
}
