import { Component, OnInit } from '@angular/core';
import { Produto } from '../models/Produto';
import { ProductService } from '../services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent implements OnInit {
  produtos: Produto[] = [];

  constructor(private productService: ProductService) { }

  ngOnInit(): void {
    this.productService.Get().subscribe(
      (success: Produto[]) => {
        console.log(success);
        this.produtos = success;
      },
      (err: any) => {
        console.warn(err);
      },
      () => { }
    );
  }
}
