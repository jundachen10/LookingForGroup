using Microsoft.AspNetCore.SignalR.Client;
using System.Windows;
using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HubConnection connection;

        public MainWindow()
        {
            InitializeComponent();
            //build desktop client app connection

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7181/chathub")//in BlazorServer launchSettings.json 
                .WithAutomaticReconnect()//auto reconnect in case we drop connection
                .Build();


            //Closed
            connection.Closed += (sender) =>
            {
                this.Dispatcher.Invoke(() =>//wrapped below inside this to ensure it invokes on the UI thread
                {
                    var newMessage = "Connection Closed";//add this message to our list of messages
                    messages.Items.Clear();
                    messages.Items.Add(newMessage);//listbox x:Name="messages"
                    openConnection.IsEnabled = true;
                    sendMessage.IsEnabled = false;//disable send message button if connection is closed
                });

                return Task.CompletedTask;
            };

            //On start connection

        }

        //The Open Connection button
        private async void openConnection_Click(object sender, RoutedEventArgs e)
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>//receivemessage trigger from clients
            {
                this.Dispatcher.Invoke(() =>
                { 
                    var newMessage = $"{user}: {message}";
                    messages.Items.Add(newMessage);
                });
            });

            try
            {
                await connection.StartAsync();// this starts the Signal R connection...if yes then do below "connection started" message
                messages.Items.Add("Connection Started");
                openConnection.IsEnabled = false;//open connection button
                sendMessage.IsEnabled = true;//sendmessage button
            }
            catch (Exception ex) // if an exception occurs display this in the UI
            {
                messages.Items.Add(ex.Message);
            }
        }

        //The Send Message Button
        private async void sendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", 
                    "WPF Client", messageInput.Text);//hard code the user
            }//grab our message and send to our hub
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }
    }
}
