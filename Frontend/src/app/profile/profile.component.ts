import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { finalize, map, tap } from 'rxjs/operators';
import { Cliente } from '../models/Cliente';
import { CacheService } from '../services/cache.service';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  cliente!: Cliente;

  constructor(
    private cacheService: CacheService,
    private cookieService: CookieService,
    private profileService: ProfileService
  ) {}

  ngOnInit(): void {
    let user: Cliente = JSON.parse(this.cookieService.get('emb_user'));        

    this.cacheService
      .GetFromCache<Cliente>(user.Id)      
      .subscribe(
        (s) => {
          this.cliente = s;
        },
        (err) => {
          console.warn(err);
        },
        () => {
          if (!this.cliente) {            
            this.profileService.GetById(user.Id).subscribe((s) => {
              this.cliente = s;
            });
          }
        }
      );
  }
}
