import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cupom } from '../models/Cupom';

@Injectable({
  providedIn: 'root'
})
export class CupomService {

  constructor(
    private http: HttpClient
  ) { }

  public VerificarValidade(descricao: string) {
    return this.http.get<Cupom>(`${environment.API}cupom/${descricao}/verificarvalidade`).pipe(first());
  }
}
