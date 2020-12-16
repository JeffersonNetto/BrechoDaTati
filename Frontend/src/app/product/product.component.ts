import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Produto } from '../models/Produto';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit {
  produto!: Produto;
  slug!: string;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private productService: ProductService
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
}
