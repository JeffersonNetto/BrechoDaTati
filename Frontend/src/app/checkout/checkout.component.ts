import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { finalize } from 'rxjs/operators';
import { Cliente } from '../models/Cliente';
import { ClienteEndereco } from '../models/ClienteEndereco';
import { Pedido } from '../models/Pedido';
import { CacheService } from '../services/cache.service';
import { PedidoService } from '../services/pedido.service';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss'],
})
export class CheckoutComponent implements OnInit {
  loading: boolean = false;
  cliente: Cliente;
  enderecoAbrev: string = 'Selecione...'
  enderecoSelecionado: string = ''
  showMessage: boolean = false
  pedido: Pedido

  constructor(
    private cacheService: CacheService,
    private cookieService: CookieService,
    private pedidoService: PedidoService
  ) {}

  ngOnInit(): void {
    let user: Cliente = JSON.parse(this.cookieService.get('emb_user'));

    this.loading = true;

    this.cacheService
      .GetFromCache<Cliente>(user.Id)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(
        (success) => {
          this.cliente = success;
        },
        (err) => {}
      );
      
      let p = localStorage.getItem('cart')      

      if(p) {
        this.pedido = JSON.parse(p);
      }
  }

  Selecionar(endereco: ClienteEndereco) {

    this.showMessage = false;

    this.enderecoSelecionado = `${endereco.Logradouro}`;

    if(endereco.Numero) {
      this.enderecoSelecionado += ', ' + endereco.Numero;
    }

    if(endereco.Complemento) {
      this.enderecoSelecionado += ', ' + endereco.Complemento;
    }

    if(endereco.Bairro) {
      this.enderecoSelecionado += ', ' + endereco.Bairro;
    }

    this.enderecoSelecionado += ', ' + endereco.Cidade;
    this.enderecoSelecionado += ' - ' + endereco.Uf;

    if(endereco.Cep) {
      this.enderecoSelecionado += ' - ' + endereco.Cep;
    }

    this.enderecoAbrev = this.enderecoSelecionado.substring(0,9) + '...'
  }

  ConfirmarPedido(){
    this.showMessage = false;    

    if(!this.enderecoSelecionado){
      this.showMessage = true;
      return;
    }

    this.loading = true;

    this.pedidoService.ConfirmarPedido(this.pedido)
    .pipe(finalize(() => this.loading = false))
    .subscribe(
      success => {
        console.log(success)

        localStorage.removeItem('cart');
        
      },
      err => {
        console.warn(err)
      }
    )
  }
}
