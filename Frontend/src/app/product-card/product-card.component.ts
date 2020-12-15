import { Component, Input, OnInit } from '@angular/core';
import { Produto } from '../models/Produto';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss']
})
export class ProductCardComponent implements OnInit {

  @Input() produto!: any;

  constructor() { }

  ngOnInit(): void {
  }

}
