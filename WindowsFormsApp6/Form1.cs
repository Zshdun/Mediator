using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WindowsFormsApp6.Form1;

namespace WindowsFormsApp6
{
    public partial class Form1 : Form
    {

         public static ChatUser user1 = new ChatUser(mediator, "User 1");
         public static ChatUser user2 = new ChatUser(mediator, "User 2");
         public static ChatUser user3 = new ChatUser(mediator, "User 3");
         public static ChatMediator mediator = new ChatMediator();


        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
           

            mediator.RegisterUser(user1);
            mediator.RegisterUser(user2);
            mediator.RegisterUser(user3);
            string msg1 = textBox1.Text;
            user1.SendMessage(msg1);
            string msg2 = textBox2.Text;
            user2.SendMessage(msg2);
            string msg3 = textBox3.Text;
            user3.SendMessage(msg3);

            List<string> user1ReceivedMessages = user1.GetReceivedMessages();
            foreach (var message in user1ReceivedMessages)
            {
                User1log.Text += message;
                User1log.Text += "\r\n";
            }
            List<string> user2ReceivedMessages = user2.GetReceivedMessages();
            foreach (var message in user2ReceivedMessages)
            {
                User2log.Text += message;
                User2log.Text += "\r\n";
            }
            List<string> user3ReceivedMessages = user3.GetReceivedMessages();
            foreach (var message in user3ReceivedMessages)
            {
                User3log.Text += message;
                User3log.Text += "\r\n";
            }

        }

        public class ChatMediator
        {
            private readonly List<IUser> _users;

            public ChatMediator()
            {
                _users= new List<IUser>();
            }

            public void RegisterUser(IUser user)
            {
                _users.Add(user);
            }
            public void TransferMessage(string senderName, string message)
            {
                foreach(var user in _users)
                {

                    if (!user.Name.Equals(senderName))
                    {
                        user.RecieveMessage(senderName, message);

                    }
                }
                

            }
        
        }
        public interface IUser
        {
             string Name { get; }

            void SendMessage(string message);

            void RecieveMessage(string senderName, string message);
        }
        public class ChatUser : IUser
        {
            private readonly ChatMediator _mediator;
            public List<string> receivedMessages;

            public ChatUser(ChatMediator mediator, string name)
            {
                _mediator = mediator;
                Name = name;
                this.receivedMessages = new List<string>();
            }
            public string Name { get; }

            public void SendMessage(string message)
            {
                //MessageBox.Show($"{Name}: Sending Message: {message}");
                MessageBox.Show("Message sent!");
                _mediator.SendMessage(senderName: Name, message: message);

            }
            public void RecieveMessage(string senderName, string message)
            {
                receivedMessages.Add($"You recieved a message : '{message}' from {senderName}");

            }
            public List<string> GetReceivedMessages()
            {
                return receivedMessages;
            }
        }

    }
}
