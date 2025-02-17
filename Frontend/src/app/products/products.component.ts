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
  pageSize: number = 9;
  page: number = 1;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {

    this.produtos = this.productService.produtos.getValue();      

    if(this.produtos.length == 0 ){
      this.productService.Get().subscribe(
        (success: Produto[]) => {       
          this.productService.produtos.next(success); 
  
          this.productService.produtos.subscribe((success) => {
            this.produtos = success;
          })        
        },
        (err: any) => {
          console.warn(err);
        },
        () => {
        }
      );
    }   
  }
}
