import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PedidoItem } from '../models/PedidoItem';
import { Produto } from '../models/Produto';
import { CartService } from '../services/cart.service';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit {
  produto!: Produto;
  slug!: string;
  estoque: number;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService,
    private toastr: ToastrService
  ) {
    this.produto = this.router.getCurrentNavigation()?.extras?.state?.produto;

    if (!this.produto) {
      this.route.params
        .subscribe((success) => {
          this.slug = success.slug;
        })
        .unsubscribe();
    }
  }

  ngOnInit(): void {
    if (this.slug) {
      this.productService.GetBySlug(this.slug).subscribe(
        (success: Produto) => {
          this.produto = success;
        },
        (err) => {
          console.log(err);
        }
      );
    }
  }

  DefinirImagemPrincipal(imagem: string) {
    this.produto.ImagemPrincipal = imagem;
  }

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
