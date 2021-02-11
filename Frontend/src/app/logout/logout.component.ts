import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss'],
})
export class LogoutComponent implements OnInit {
  constructor(
    private loginService: LoginService,
    private router: Router,
    private cookieService: CookieService
    ) {}

  ngOnInit(): void {
    this.loginService.isUserLogged.next(false);

    this.cookieService.delete('emb_user');

    this.router.navigate(['/home']);
  }
}
