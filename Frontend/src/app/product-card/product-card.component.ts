import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
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

  constructor(
    private cartService: CartService,
    private toastr: ToastrService
    ) {}

  ngOnInit(): void {}

  Adicionar() {

    let pedidoItem: PedidoItem = {
      PedidoId: undefined,
      Produto: this.produto,
      ProdutoId: this.produto.Id,
      Quantidade: 1,
      ValorUnitario: this.produto.ValorVenda,
      ValorUnitarioPago: this.produto.ValorPromocional || this.produto.ValorVenda,
    };

    this.cartService.Adicionar(pedidoItem);

    this.toastr.success('Produto adicionado ao carrinho', pedidoItem.Produto.Nome, {
      positionClass: 'toast-bottom-center',
    });
  }
}
