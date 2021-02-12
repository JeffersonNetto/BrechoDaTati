import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject } from 'rxjs';
import { Cliente } from '../models/Cliente';
import { Pedido } from '../models/Pedido';
import { PedidoItem } from '../models/PedidoItem';
import { ProductService } from './product.service';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  carrinho: BehaviorSubject<Pedido>;

  constructor(
    private cookieService: CookieService,
    private productService: ProductService
  ) {
    if (!this.carrinho && this.cookieService.check('emb_user')) {
      let user: Cliente = JSON.parse(this.cookieService.get('emb_user'));

      let pedido: Pedido = {
        ClienteId: user.Id || undefined,
        CupomId: undefined,
        DataCriacao: undefined,
        Id: undefined,
        StatusId: 1,
        PedidoItem: [],
        Ip: undefined,
      };

      this.carrinho = new BehaviorSubject(pedido);
    }
  }

  Adicionar(pedidoItem: PedidoItem) {
    let p = this.carrinho.getValue();

    let item = p.PedidoItem.find((_) => _.ProdutoId == pedidoItem.ProdutoId);
    
    if (item) {  
      // if(item.Produto.Estoque > 0) {
      //   item.Produto.Estoque--;
      // }    

      item.Quantidade++;
    } else {

      // if(pedidoItem.Produto.Estoque > 0) {
      //   pedidoItem.Produto.Estoque--;
      // }
      
      p.PedidoItem.push(pedidoItem);
    }

    this.carrinho.next(p);

    localStorage.setItem('cart', JSON.stringify(p));

    // this.productService
    //   .DecrementarEstoque(pedidoItem.ProdutoId)
    //   .subscribe((success) => {
    //     this.productService.produtos.next(success);
    //   });
  }

  Remover(pedidoItem: PedidoItem) {
    let pedido = this.carrinho.getValue();

    let i = pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == pedidoItem.ProdutoId
    );

    // this.productService
    //   .IncrementarEstoque(pedido.PedidoItem[i].ProdutoId)
    //   .subscribe((success) => {
    //     this.productService.produtos.next(success);
    //   });

    pedido.PedidoItem.splice(i, 1);

    this.carrinho.next(pedido);

    localStorage.setItem('cart', JSON.stringify(pedido));    
  }
}
