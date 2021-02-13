import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Pedido } from '../models/Pedido';

@Injectable({
  providedIn: 'root'
})
export class PedidoService {

  constructor(
    private http: HttpClient    
    ) { }

    ConfirmarPedido(pedido: Pedido) {

      // pedido.PedidoItem.forEach(item => {
      //   item.Produto = null;
      // });

      return this.http.post(`${environment.API}pedido`, pedido).pipe(first());
    }
}
