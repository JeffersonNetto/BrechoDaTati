import { Component, Input, OnInit } from '@angular/core';
import { PedidoItem } from '../models/PedidoItem';
import { Produto } from '../models/Produto';
import { CartService } from '../services/cart.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
})
export class ProductCardComponent implements OnInit {
  @Input() produto!: Produto;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {}

  Adicionar() {

    let pedidoItem: PedidoItem = {
      PedidoId: undefined,
      Produto: this.produto,
      ProdutoId: this.produto.Id,
      Quantidade: 1,
      ValorUnitario: this.produto.ValorVenda,
      ValorUnitarioPago: this.produto.ValorVenda,
    };

    this.cartService.Adicionar(pedidoItem);
  }
}
