import { Component, OnInit } from '@angular/core';
import { delay, finalize } from 'rxjs/operators';
import { Cupom } from '../models/Cupom';
import { Pedido } from '../models/Pedido';
import { PedidoItem } from '../models/PedidoItem';
import { CartService } from '../services/cart.service';
import { CupomService } from '../services/cupom.service';
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
  descontoDoCupom = 0;
  inputCupom: string;
  cupomAplicado: Cupom;
  loading: boolean = false;

  constructor(
    private cartService: CartService,
    private productService: ProductService,
    private cupomService: CupomService
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

      this.productService
        .IncrementarEstoque(this.pedido.PedidoItem[i].ProdutoId)
        .subscribe((success) => {
          this.productService.produtos.next(success);
        });
    }

    this.cartService.carrinho.next(this.pedido);

    localStorage.setItem('cart', JSON.stringify(this.pedido));
  }

  Aumentar(item: PedidoItem) {
    let i = this.pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == item.ProdutoId
    );

    if (item.Produto.Estoque > 0) {
      this.pedido.PedidoItem[i].Quantidade++;
      this.pedido.PedidoItem[i].Produto.Estoque--;

      this.productService
        .DecrementarEstoque(this.pedido.PedidoItem[i].ProdutoId)
        .subscribe((success) => {
          this.productService.produtos.next(success);
        });

      this.pedido.PedidoItem[i].Produto.Estoque--;
    }

    this.cartService.carrinho.next(this.pedido);

    localStorage.setItem('cart', JSON.stringify(this.pedido));
  }

  Remover(item: PedidoItem) {
    this.cartService.Remover(item);
  }

  FinalizarCompra() {}

  CalcularResumo() {
    this.total = 0;
    this.subTotal = 0;
    this.desconto = 0;

    this.pedido.PedidoItem.forEach((_) => {
      this.desconto += _.Quantidade * (_.ValorUnitario - _.ValorUnitarioPago);
      this.subTotal += _.Quantidade * _.ValorUnitario;
    });

    this.total = this.subTotal - this.desconto;

    if (this.cupomAplicado) {
      this.descontoDoCupom = this.total * (this.cupomAplicado.Desconto / 100);

      this.total =
        this.total - this.total * (this.cupomAplicado.Desconto / 100);
    }
  }

  AplicarDesconto() {
    this.loading = true;    
    this.cupomService
      .VerificarValidade(this.inputCupom)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(
        (success) => {
          this.cupomAplicado = success;

          this.CalcularResumo();
        },
        (err) => {},
        () => {}
      );
  }
}
