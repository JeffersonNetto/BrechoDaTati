import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { finalize } from 'rxjs/operators';
import { Cliente } from '../models/Cliente';
import { CacheService } from '../services/cache.service';

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

  constructor(
    private cacheService: CacheService,
    private cookieService: CookieService
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
  }

  Selecionar(endereco) {
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
}
