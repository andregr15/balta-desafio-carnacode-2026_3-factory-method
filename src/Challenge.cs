// DESAFIO: Sistema de Notificações Multi-Canal
// PROBLEMA: Uma aplicação de e-commerce precisa enviar notificações por diferentes canais
// (Email, SMS, Push, WhatsApp) dependendo da preferência do cliente e tipo de notificação
// O código atual viola o Open/Closed Principle ao usar condicionais para criar notificações
namespace DesignPatternChallenge
{
    // Contexto: Sistema de notificações que envia mensagens para clientes
    // Cada tipo de notificação tem requisitos e formatação diferentes

    public interface INotification
    {
        void Send();
    }

    public abstract class OrderConfirmationNotificationCreator
    {
        public abstract INotification CreateNotification(string recipient, string orderNumber);

        public void SendNotification(string recipient, string orderNumber)
        {
            var notification = CreateNotification(recipient, orderNumber);
            notification.Send();
        }
    }

    public abstract class ShippingUpdateNotificationCreator
    {
        public abstract INotification CreateNotification(string recipient, string trackingCode);

        public void SendNotification(string recipient, string trackingCode)
        {
            var notification = CreateNotification(recipient, trackingCode);
            notification.Send();
        }
    }

    public abstract class PaymentReminderNotificationCreator
    {
        public abstract INotification CreateNotification(string recipient, decimal amount);

        public void SendNotification(string recipient, decimal amount)
        {
            var notification = CreateNotification(recipient, amount);
            notification.Send();
        }

    }

    public class EmailOrderConfirmationNotificationCreator : OrderConfirmationNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string orderNumber)
        {
            return new EmailNotification
            {
                Recipient = recipient,
                Subject = "Confirmação de Pedido",
                Body = $"Seu pedido {orderNumber} foi confirmado!",
                IsHtml = true
            };
        }
    }

    public class EmailShippingUpdateNotificationCreator : ShippingUpdateNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string trackingCode)
        {
            return new EmailNotification
            {
                Recipient = recipient,
                Subject = "Pedido Enviado",
                Body = $"Seu pedido foi enviado! Código de rastreamento: {trackingCode}",
                IsHtml = true
            };
        }
    }

    public class EmailPaymentReminderNotificationCreator : PaymentReminderNotificationCreator
    {
        public override INotification CreateNotification(string recipient, decimal amount)
        {
            return new EmailNotification
            {
                Recipient = recipient,
                Subject = "Lembrete de Pagamento",
                Body = $"Você tem um pagamento pendente de R$ {amount:N2}",
                IsHtml = true
            };
        }
    }

    public class SmsOrderConfirmationNotificationCreator : OrderConfirmationNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string orderNumber)
        {
            return new SmsNotification
            {
                PhoneNumber = recipient,
                Message = $"Pedido {orderNumber} confirmado!",
            };
        }
    }

    public class SmsShippingUpdateNotificationCreator : ShippingUpdateNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string trackingCode)
        {
            return new SmsNotification
            {
                PhoneNumber = recipient,
                Message = $"Pedido enviado! Rastreamento: {trackingCode}",
            };
        }
    }

    public class SmsPaymentReminderNotificationCreator : PaymentReminderNotificationCreator
    {
        public override INotification CreateNotification(string recipient, decimal amount)
        {
            return new SmsNotification
            {
                PhoneNumber = recipient,
                Message = $"Pagamento pendente: R$ {amount:N2}",
            };
        }
    }

    public class PushOrderConfirmationNotificationCreator : OrderConfirmationNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string orderNumber)
        {
            return new PushNotification
            {
                DeviceToken = recipient,
                Title = "Pedido Confirmado",
                Message = $"Pedido {orderNumber} confirmado!",
                Badge = 1
            };
        }
    }

    public class PushShippingUpdateNotificationCreator : ShippingUpdateNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string trackingCode)
        {
            return new PushNotification
            {
                DeviceToken = recipient,
                Title = "Pedido Enviado",
                Message = $"Rastreamento: {trackingCode}",
                Badge = 1
            };
        }
    }

    public class PushPaymentReminderNotificationCreator : PaymentReminderNotificationCreator
    {
        public override INotification CreateNotification(string recipient, decimal amount)
        {
            return new PushNotification
            {
                DeviceToken = recipient,
                Title = "Lembrete de Pagamento",
                Message = $"Pagamento pendente: R$ {amount:N2}",
                Badge = 1
            };
        }
    }

    public class WhatsAppOrderConfirmationNotificationCreator : OrderConfirmationNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string orderNumber)
        {
            return new WhatsAppNotification
            {
                PhoneNumber = recipient,
                Message = $"✅ Seu pedido {orderNumber} foi confirmado!",
                UseTemplate = true
            };
        }
    }

    public class WhatsAppShippingUpdateNotificationCreator : ShippingUpdateNotificationCreator
    {
        public override INotification CreateNotification(string recipient, string trackingCode)
        {
            return new WhatsAppNotification
            {
                PhoneNumber = recipient,
                Message = $"📦 Pedido enviado! Rastreamento: {trackingCode}",
                UseTemplate = true
            };
        }
    }

    public class WhatsAppPaymentReminderNotificationCreator : PaymentReminderNotificationCreator
    {
        public override INotification CreateNotification(string recipient, decimal amount)
        {
            return new WhatsAppNotification
            {
                PhoneNumber = recipient,
                Message = $"⚠️ Lembrete: pagamento pendente de R$ {amount:N2}",
                UseTemplate = true
            };
        }
    }

    // Classes concretas de notificação
    public class EmailNotification : INotification
    {
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }

        public void Send()
        {
            Console.WriteLine($"📧 Enviando Email para {Recipient}");
            Console.WriteLine($"   Assunto: {Subject}");
            Console.WriteLine($"   Mensagem: {Body}");
        }
    }

    public class SmsNotification : INotification
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }

        public void Send()
        {
            Console.WriteLine($"📱 Enviando SMS para {PhoneNumber}");
            Console.WriteLine($"   Mensagem: {Message}");
        }
    }

    public class PushNotification : INotification
    {
        public string DeviceToken { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public int Badge { get; set; }

        public void Send()
        {
            Console.WriteLine($"🔔 Enviando Push para dispositivo {DeviceToken}");
            Console.WriteLine($"   Título: {Title}");
            Console.WriteLine($"   Mensagem: {Message}");
        }
    }

    public class WhatsAppNotification : INotification
    {
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public bool UseTemplate { get; set; }

        public void Send()
        {
            Console.WriteLine($"💬 Enviando WhatsApp para {PhoneNumber}");
            Console.WriteLine($"   Mensagem: {Message}");
            Console.WriteLine($"   Template: {UseTemplate}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Notificações ===\n");

            // Cliente 1 prefere Email
            var emailOrderConfirmationCreator = new EmailOrderConfirmationNotificationCreator();
            var emailOrderNotification = emailOrderConfirmationCreator.CreateNotification("cliente@email.com", "12345");
            emailOrderNotification.Send();

            Console.WriteLine();

            // Cliente 2 prefere SMS
            var smsOrderConfirmationCreator = new SmsOrderConfirmationNotificationCreator();
            var smsOrderNotification = smsOrderConfirmationCreator.CreateNotification("+5511999999999", "12346");
            smsOrderNotification.Send();

            Console.WriteLine();

            // Cliente 3 prefere Push
            var pushShippingUpdateCreator = new PushShippingUpdateNotificationCreator();
            var pushShippingNotification = pushShippingUpdateCreator.CreateNotification("device-token-abc123", "BR123456789");
            pushShippingNotification.Send();

            Console.WriteLine();

            // Cliente 4 prefere WhatsApp
            var whatsappPaymentReminderCreator = new WhatsAppPaymentReminderNotificationCreator();
            var whatsappPaymentNotification = whatsappPaymentReminderCreator.CreateNotification("+5511888888888", 150.00m);
            whatsappPaymentNotification.Send();

            // Perguntas para reflexão:
            // - Como adicionar novos tipos de notificação (Telegram, Slack) sem modificar NotificationManager?
            // - Como evitar duplicação da lógica condicional em cada método?
            // - Como permitir que subclasses decidam qual tipo de notificação criar?
            // - Como tornar o código mais extensível e manutenível?
        }
    }
}
