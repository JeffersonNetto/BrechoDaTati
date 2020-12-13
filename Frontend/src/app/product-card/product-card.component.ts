import { Component, Input, OnInit } from '@angular/core';
import { Produto } from '../models/produto';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() produto!: Produto;

  constructor() { }

  ngOnInit(): void {
  }

}
