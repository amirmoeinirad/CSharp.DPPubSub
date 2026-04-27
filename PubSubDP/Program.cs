// Amir Moeini Rad
// April 2026

// Main Concept: The Publisher-Subscriber Design Pattern in C#.

// A fundamental messaging pattern used heavily in distributed systems and event-driven architectures.
// Structure:
// - Publisher: Sends a message to a list of subscribers
// - Subscriber(s): Receive(s) a message produced by the publisher
// - Event Bus / Message Broker: Delivers the message produced by the publisher to the subscribers.

namespace PubSubDP
{
    // The Message Broker: sends messages between a publisher and its subscribers
    public class EventBus
    {
        // A dictionary in which for each topic like 'news', there is a list of subscriber handlers.
        private readonly Dictionary<string, List<Action<string>>> _subscriptions = [];

        // Registers a subscriber (a handler method) in the list for a topic
        public void Subscribe(string topic, Action<string> handler)
        {
            if (!_subscriptions.ContainsKey(topic))
            {
                _subscriptions[topic] = [];
            }

            _subscriptions[topic].Add(handler);
        }

        // Sends a message to all the subscribers / handlers of a topic.
        public void Publish(string topic, string message)
        {
            if (_subscriptions.ContainsKey(topic))
            {
                foreach (var handler in _subscriptions[topic])
                {
                    handler(message);
                }
            }
        }
    }

    // -------------------------------------------------

    // Publisher
    public class Publisher
    {
        private readonly EventBus _bus;

        public Publisher(EventBus bus)
        {
            _bus = bus;
        }

        // Delivering the message to the event bus / message broker
        public void Send(string topic, string message)
        {
            Console.WriteLine($"Publishing a message: '{message}'");
            _bus.Publish(topic, message);
        }
    }

    // -------------------------------------------------

    // Subscribers
    public class SubscriberA
    {
        // Receiving the message from the vent bus / message broker
        public void Handle(string message)
        {
            Console.WriteLine($"Subscriber A received the message: {message}");
        }
    }

    public class SubscriberB
    {
        public void Handle(string message)
        {
            Console.WriteLine($"Subscriber B received the message: {message}");
        }
    }

    // -------------------------------------------------

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("The Publisher-Subscriber Design Pattern in C#.NET.");
            Console.WriteLine("--------------------------------------------------\n");

            var eventBus = new EventBus();

            var subA = new SubscriberA();
            var subB = new SubscriberB();

            // Subscribe to topics
            eventBus.Subscribe("news", subA.Handle);
            eventBus.Subscribe("news", subB.Handle);

            var publisher = new Publisher(eventBus);

            // Publish message
            publisher.Send("news", "New event happened!");

            Console.WriteLine("\nDone.");
        }
    }
}
