import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ClienteEndereco } from '../models/ClienteEndereco';
import { Retorno } from '../models/Retorno';

@Injectable({
  providedIn: 'root',
})
export class AddressService {
  constructor(private http: HttpClient) {}

  GetAddressViaCep(cep: string) {    
    return this.http.get(`//viacep.com.br/ws/${cep}/json`).pipe(first());
  }

  UpdateAddress(endereco: ClienteEndereco) {
    return this.http
      .put<Retorno<ClienteEndereco>>(`${environment.API}cliente/endereco/${endereco.Id}`, endereco)
      .pipe(first());
  }
}
