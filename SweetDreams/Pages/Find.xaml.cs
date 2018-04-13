using System;
using System.Collections.Generic;
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
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Collections.Concurrent;

namespace SweetDreams.Pages
{
    /// <summary>
    /// Логика взаимодействия для Find.xaml
    /// </summary>
    public partial class Find : Page
    {

        public static Label findStat;
        public static string resp;
        public static Button submitSearch;
        public Find()
        {
            InitializeComponent();
            findStat = StatusFind;
            submitSearch = SubmitPairSearch;
        }

        private void SubmitPairSearch_Click(object sender, RoutedEventArgs e)
        {
            submitSearch.IsEnabled = false;
            Task TaskForSending = new Task(() => SendToQueue(MainWindow.CurrentUser));
            TaskForSending.Start();
            TaskForSending.Wait();
            findStat.Content = "Wait until we find you somebody...";
            findStat.Content = resp;
        }

        public static void SendToQueue(User user)
        {
                   
            var rpcClient = new RpcClient(user.PersonalKey.ToString());
            string message = user.UserID + " " + user.PersonalKey + " " + user.Gender + " " + user.LookingFor;
            var response = rpcClient.Call(message);
            resp = response;
            int result = 0;           
            if(Int32.TryParse(resp, out result))
            {
                if (result == 0)
                {
                    resp = "Sorry, no mathces, you such a looser";
                }
                else
                {
                    YeahWeHaveAPair(result);
                }
            }
            
            rpcClient.Close();
        }
        public class RpcClient
        {
            private readonly IConnection connection;
            private readonly IModel channel;
            private readonly string replyQueueName;
            private readonly EventingBasicConsumer consumer;
            private readonly BlockingCollection<string> respQueue = new BlockingCollection<string>();
            private readonly IBasicProperties props;

            public RpcClient(string PersonalID)
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };

                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                replyQueueName = channel.QueueDeclare().QueueName;
                consumer = new EventingBasicConsumer(channel);

                props = channel.CreateBasicProperties();
                var correlationId = PersonalID;
                props.CorrelationId = correlationId;
                props.ReplyTo = replyQueueName;

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var response = Encoding.UTF8.GetString(body);
                    if (ea.BasicProperties.CorrelationId == correlationId)
                    {
                        respQueue.Add(response);
                    }
                };
            }

            public string Call(string message)
            {
                var messageBytes = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(
                    exchange: "",
                    routingKey: "q1",
                    basicProperties: props,
                    body: messageBytes);

                channel.BasicConsume(
                    consumer: consumer,
                    queue: replyQueueName,
                    autoAck: true);

                return respQueue.Take(); ;
            }

            public void Close()
            {
                connection.Close();
            }
        }

        public static void YeahWeHaveAPair(int pairId)
        {
            foreach (var item in MainWindow.UsersList)
            {
                if (item.UserID == pairId)
                {
                    resp = "Name: " + item.Name + " Phone: "+item.PhoneNumber;
                    break;
                }

            }

        }
    }    
 }
