import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PedidoItem } from '../models/PedidoItem';
import { Produto } from '../models/Produto';
import { CartService } from '../services/cart.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
})
export class ProductCardComponent implements OnInit {
  @Input() produto!: Produto;
  estoque: number;

  constructor(
    private cartService: CartService,
    private toastr: ToastrService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {}

  Adicionar() {
    this.productService.VerificarEstoque(this.produto.Id).subscribe(
      (success: number) => {
        this.estoque = success;
      },
      (err) => {
        this.toastr.error(
          'Não foi possível obter o estoque do produto',
          this.produto.Nome,
          {
            positionClass: 'toast-bottom-center',
          }
        );
      },
      () => {
        if (this.estoque == 0) {
          this.toastr.info('Produto esgotado', this.produto.Nome, {
            positionClass: 'toast-bottom-center',
          });

          this.produto.Estoque = this.estoque;

        } else {
          let pedidoItem: PedidoItem = {
            PedidoId: undefined,
            Produto: this.produto,
            ProdutoId: this.produto.Id,
            Quantidade: 1,
            ValorUnitario: this.produto.ValorVenda,
            ValorUnitarioPago:
              this.produto.ValorPromocional || this.produto.ValorVenda,
          };

          this.cartService.Adicionar(pedidoItem);          

          this.toastr.success(
            'Produto adicionado ao carrinho',
            pedidoItem.Produto.Nome,
            {
              positionClass: 'toast-bottom-center',
            }
          );
        }
      }
    );
  }
}
