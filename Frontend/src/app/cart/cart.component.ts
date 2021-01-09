import { Component, OnInit } from '@angular/core';
import { Pedido } from '../models/Pedido';
import { PedidoItem } from '../models/PedidoItem';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss'],
})
export class CartComponent implements OnInit {
  pedido: Pedido;
  subTotal = 0;
  total = 0;
  desconto = 0;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    //this.pedido = this.cartService.carrinho?.getValue();

    this.cartService.carrinho?.subscribe(success => {
      this.pedido = success;
      this.CalcularResumo();
    });
  }

  Diminuir(item: PedidoItem) {
    let i = this.pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == item.ProdutoId
    );

    if (this.pedido.PedidoItem[i].Quantidade > 1) {
      this.pedido.PedidoItem[i].Quantidade -= 1;
    }

    this.cartService.carrinho.next(this.pedido);
  }

  Aumentar(item: PedidoItem) {
    let i = this.pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == item.ProdutoId
    );
    this.pedido.PedidoItem[i].Quantidade += 1;

    this.cartService.carrinho.next(this.pedido);
  }

  Remover(item: PedidoItem) {
    let i = this.pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == item.ProdutoId
    );

    this.pedido.PedidoItem.splice(i, 1);

    this.cartService.carrinho.next(this.pedido);
  }

  FinalizarCompra() {
    console.log('finalizar compra')
  }

  CalcularResumo() {
    this.total = 0;
    this.subTotal = 0;
    this.desconto = 0;

    console.log(this.pedido);

    this.pedido.PedidoItem.forEach((_) => {
      this.desconto += _.Quantidade * (_.ValorUnitario - _.ValorUnitarioPago);
      this.subTotal += _.Quantidade * _.ValorUnitario;
    });

    console.log('subTotal', this.subTotal);
    console.log('total', this.total);

    this.total = this.subTotal - this.desconto;

    console.log('desconto', this.desconto);
  }
}
