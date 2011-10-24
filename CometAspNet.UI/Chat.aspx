<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chat.aspx.cs" Inherits="CometAspNet.UI.Chat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="Scripts/jquery-1.6.4.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript">
        $(function () {
            $('#btSend').click(function () {
                $.post('Handlers/Send.ashx', {
                    SubscriberId: '<%= Request.QueryString["SubscriberId"]%>',
                    Text: $('#tbMessage').val()
                });
            });
            var waitForMessage = function () { };
            waitForMessage = function (data) {
                if (data && data.messages) {
                    $.each(data.messages, function (index, value) {
                        var container = $('.messages');
                        if (value.Message) {
                            container.prepend('<p><b>' + value.Message + '</b></p>');
                        } else {
                            container.prepend('<p><b>' + value.From + "</b>: " + value.Text + '</p>');
                        }
                    });
                }

                $.ajax({
                    url: 'Handlers/LongPulling.ashx?SubscriberId=<%= Request.QueryString["SubscriberId"]%>',
                    //url: 'Handlers/LongPulling-WrongWay.ashx?SubscriberId=<%= Request.QueryString["SubscriberId"]%>',
                    dataType: "json",
                    success: waitForMessage,
                    error: waitForMessage
                });
            };

            waitForMessage();
        });
    </script>
    <div>
        <input id="tbMessage" type="text" />
        <input id="btSend" type="button" value="Send" />
        <div class="messages">
        </div>
    </div>
    </form>
</body>
</html>
