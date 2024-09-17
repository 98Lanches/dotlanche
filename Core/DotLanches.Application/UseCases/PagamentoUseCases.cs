using DotLanches.Domain.Entities;
using DotLanches.Domain.Interfaces.ExternalInterfaces;
using DotLanches.Domain.Interfaces.Gateways;

namespace DotLanches.Application.UseCases
{
    public static class PagamentoUseCases
    {
        public static async Task<string> RequestQrCodeForPedido(Guid idPedido, IPedidoGateway pedidoGateway, IPagamentoGateway pagamentoGateway, ICheckout checkout)
        {
            var pedido = await pedidoGateway.GetById(idPedido) ??
                throw new Exception("Payment processing error: Non existing pedido!");

            if (pedido.Status != EStatus.confirmado) throw new Exception("Payment processing error: Pedido not confirmed!");

            await pagamentoGateway.Add(pedido.Pagamento);

            //Fake Checkout for current version
            var qrCode = checkout.RequestQrCode(pedido.Pagamento);

            return qrCode;
        }

        public static async Task<QueueKey> AcceptedPagamento(Guid idPedido, IPedidoGateway pedidoGateway, IPagamentoGateway pagamentoGateway)
        {
            var pedido = await pedidoGateway.GetById(idPedido) ??
                throw new Exception("Payment processing error: Non existing pedido!");

            if (pedido.Status != EStatus.confirmado)
                throw new Exception("Payment processing error: Pedido not confirmed!");

            pedido.Pagamento.ConfirmPayment();

            var lastQueueKey = await pedidoGateway.GetLastQueueKeyNumber();
            var newQueueKey = new QueueKey(lastQueueKey + 1, pedido.Pagamento.RegisteredAt!.Value);

            pedido.ReceivePagamento(newQueueKey);

            await pagamentoGateway.Update(pedido.Pagamento);
            await pedidoGateway.Update(pedido);

            return newQueueKey;
        }

        public static async Task<Pagamento> RefusedPagamento(Guid idPedido, IPedidoGateway pedidoGateway, IPagamentoGateway pagamentoGateway)
        {
            var pedido = await pedidoGateway.GetById(idPedido) ??
                throw new Exception("Payment processing error: Non existing pedido!");

            if (pedido.Status != EStatus.confirmado)
                throw new Exception("Payment processing error: Pedido not confirmed!");

            var pagamento = await pagamentoGateway.GetByIdPedido(pedido.Id);
            pagamento.RefusePayment();

            pagamento = await pagamentoGateway.Update(pagamento);
            return pagamento;
        }

        public static async Task<Pagamento> GetByIdPedido(Guid idPedido, IPagamentoGateway pagamentoGateway)
        {
            return await pagamentoGateway.GetByIdPedido(idPedido);
        }
    }
}