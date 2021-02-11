import { Component, OnInit } from '@angular/core';
import { Pedido } from '../models/Pedido';
import { PedidoItem } from '../models/PedidoItem';
import { CartService } from '../services/cart.service';
import { ProductService } from '../services/product.service';

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

  constructor(
    private cartService: CartService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    //this.pedido = this.cartService.carrinho?.getValue();

    this.cartService.carrinho?.subscribe((success) => {
      this.pedido = success;
      this.CalcularResumo();
    });
  }

  Diminuir(item: PedidoItem) {
    let i = this.pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == item.ProdutoId
    );

    if (this.pedido.PedidoItem[i].Quantidade > 1) {
      this.pedido.PedidoItem[i].Quantidade--;
      this.pedido.PedidoItem[i].Produto.Estoque++;

      this.productService.IncrementarEstoque(this.pedido.PedidoItem[i].ProdutoId).subscribe(success => {
        this.productService.produtos.next(success);  
      })
    }

    this.cartService.carrinho.next(this.pedido);

    localStorage.setItem('cart', JSON.stringify(this.pedido));
  }

  Aumentar(item: PedidoItem) {    
    
    let i = this.pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == item.ProdutoId
    );    

    if (item.Produto.Estoque > 0){
      this.pedido.PedidoItem[i].Quantidade++;   
      this.pedido.PedidoItem[i].Produto.Estoque--;   

      this.productService.DecrementarEstoque(this.pedido.PedidoItem[i].ProdutoId).subscribe(success => {
        this.productService.produtos.next(success);          
      }) 

      this.pedido.PedidoItem[i].Produto.Estoque--;
    }      

    this.cartService.carrinho.next(this.pedido);

    localStorage.setItem('cart', JSON.stringify(this.pedido));
  }

  Remover(item: PedidoItem) {
    this.cartService.Remover(item);
  }

  FinalizarCompra() {

  }

  CalcularResumo() {
    this.total = 0;
    this.subTotal = 0;
    this.desconto = 0;

    this.pedido.PedidoItem.forEach((_) => {
      this.desconto += _.Quantidade * (_.ValorUnitario - _.ValorUnitarioPago);
      this.subTotal += _.Quantidade * _.ValorUnitario;
    });

    this.total = this.subTotal - this.desconto;
  }
}
