import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Produto } from '../models/Produto';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
})
export class ProductComponent implements OnInit {
  produto!: Produto;

  constructor(private router: Router) {
    this.produto = this.router.getCurrentNavigation()?.extras?.state?.produto;    
  }

  ngOnInit(): void {}
}
