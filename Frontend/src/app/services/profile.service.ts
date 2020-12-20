import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { first } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Cliente } from '../models/Cliente';
import { Retorno } from '../models/Retorno';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {

  constructor(
    private http: HttpClient,
    private cookieService: CookieService    
  ) { }
  
  public GetById(Id: string) {    
    return this.http.get<Cliente>(`${environment.API}cliente/${Id}`).pipe(first());        
  }

  public Update(cliente: Cliente) {
    return this.http.put<Retorno<Cliente>>(`${environment.API}cliente/${cliente.Id}`, cliente).pipe(first());
  }
}
