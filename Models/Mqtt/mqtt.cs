using Microsoft.Extensions.Configuration;
using SimpleMqtt;

namespace Models
{
    public class MqttUser
    {
        //VUL DIT IN(gebruik de gegevens van de mqtt broker en zet het in SimpleMqttClient.cs):
        // var mqttWrapper = new SimpleMqttClient(new()
        // {
        //     Host = "", // maak eventueel een account aan bij hivemq als dit problemen geeft.
        //     Port = 8883,
        //     CleanStart = false, // <--- false, haalt al gebufferde meldingen ook op.
        //     ClientId = clientId, // Dit clientid moet uniek zijn binnen de broker
        //     TimeoutInMs = 5_000, // Standaard time-out bij het maken van een verbinding (5 seconden)
        //     UserName = "",
        //     Password = ""
        // });
        public async Task RunMqttClient()
        {
            var client = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("client-id");

            var receive = new MqttReceiver(client);
            receive.Receive();

            string topic = "TestMQtt";
            await client.SubscribeToTopic(topic);
            Console.WriteLine($"Subscribed to topic: {topic}\n");

            string text = string.Empty;
            var publish = new MqttPublisher(client)
            {
                Topic = topic,
                Message = text
            };

            Console.WriteLine("Your text:");
            text = Console.ReadLine() ?? string.Empty;
            publish.Message = text;

            publish.Publish();

            Console.WriteLine("Press any key to continue or 'q' to quit.");
            if (Console.ReadKey().KeyChar == 'q')
            {
                return;
            }
            Console.ReadKey();
        }
    }
}
