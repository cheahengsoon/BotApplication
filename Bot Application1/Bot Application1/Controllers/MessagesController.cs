using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
//using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;
using Microsoft.Bot.Builder.Dialogs;
namespace Bot_Application1
{
      [Serializable]
    public class HelpDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(YourFunctionForMessageReceived);
        }
        public async Task YourFunctionForMessageReceived(IDialogContext context, IAwaitable<Message> argument)
        {
            var message = await argument;
            if (message.Text == "help")
            {
                PromptDialog.Confirm(
                    context,
                    YourFunctionForHelpCommand,
                    "Are you sure you want to display help?",
                    "Didn't get that!");
            }
            else if(message.Text=="xamarin")
            {
                await context.PostAsync("XAmarin tutorial");
                context.Wait(YourFunctionForMessageReceived);

              
            }
         
     
            else
            {
               // int length = (message.Text ?? string.Empty).Length;
              //  await context.PostAsync($"You sent {length} characters");
                context.Wait(YourFunctionForMessageReceived);
            }
        }
        public async Task YourFunctionForHelpCommand(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                await context.PostAsync("I am a simple bot that will display the length of your messages! You can display this message by typing \"help\"!");
            }
            context.Wait(YourFunctionForMessageReceived);
        }
    }
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<Message> Post([FromBody]Message message)
        {
            if (message.Type == "Message")
            {
                // calculate something for us to return
                //int length = (message.Text ?? string.Empty).Length;

                // return our reply to the user
                //return message.CreateReplyMessage("You Just Say "+message.Text);
                string FoodItem = (message.Text).ToLower();
                if (FoodItem == "pizza")
                {
                    Attachment attachment1 = new Attachment();
                    attachment1.ContentType = "image/png";
                    attachment1.ContentUrl = "http://www.lapizzatreno.com/upload/pizza_thumb/pizza_image2376.png";

                    Attachment attachment2 = new Attachment();
                    attachment2.ContentType = "image/png";
                    attachment2.ContentUrl = "https://mob.kotipizza.fi/kuvat/tuotteet/pizza-berlusconi-200x200.png";

                    message.Attachments.Add(attachment1);
                    message.Attachments.Add(attachment2);

                    message.Text = "Which pizza you want ";
                    return message;
                }
                return message;
            }


            else
            {
                return HandleSystemMessage(message);
            }
            }
        
 
        private Message HandleSystemMessage(Message message)
        {
            if (message.Type == "Ping")
            {
                Message reply = message.CreateReplyMessage();
                reply.Type = "Ping";
                return reply;
            }
            else if (message.Type == "DeleteUserData")
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == "BotAddedToConversation")
            {
            }
            else if (message.Type == "BotRemovedFromConversation")
            {
            }
            else if (message.Type == "UserAddedToConversation")
            {
            }
            else if (message.Type == "UserRemovedFromConversation")
            {
            }
            else if (message.Type == "EndOfConversation")
            {
            }
 
            return null;
        }
    }
}