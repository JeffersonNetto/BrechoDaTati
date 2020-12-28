import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject } from 'rxjs';
import { Cliente } from '../models/Cliente';
import { Pedido } from '../models/Pedido';
import { PedidoItem } from '../models/PedidoItem';

@Injectable({
  providedIn: 'root',
})
export class CartService {
  carrinho: BehaviorSubject<Pedido>;

  constructor(private cookieService: CookieService) {

    if(!this.carrinho && this.cookieService.check('emb_user')){
      let user: Cliente = JSON.parse(this.cookieService.get('emb_user'));
    
      let pedido: Pedido = {
        ClienteId:
          user.Id || undefined,
        CupomId: undefined,
        DataCriacao: undefined,
        Id: undefined,
        StatusId: 1,
        PedidoItem: [],
      };

      this.carrinho = new BehaviorSubject(pedido);        
    }    
  }

  Adicionar(pedidoItem: PedidoItem) {    
    let p = this.carrinho.getValue();

    p.PedidoItem.push(pedidoItem);

    this.carrinho.next(p);    
  }

  Remover(pedidoItem: PedidoItem) {
    let pedido = this.carrinho.getValue();

    let index = pedido.PedidoItem.findIndex(
      (_) => _.ProdutoId == pedidoItem.ProdutoId
    );

    pedido.PedidoItem.splice(index);

    this.carrinho.next(pedido);
  }
}
