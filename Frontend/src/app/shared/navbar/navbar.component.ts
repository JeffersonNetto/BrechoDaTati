import { AfterContentInit, AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { Location, PopStateEvent } from '@angular/common';
import { CartService } from 'src/app/services/cart.service';
import { first, take } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';
import { LoginService } from 'src/app/services/login.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit, OnDestroy, AfterContentInit {
  public isCollapsed = true;
  private lastPoppedUrl!: string;
  private yScrollStack: number[] = [];
  cartItems: number = 0;
  isUserLogged: boolean = false;

  constructor(
    public location: Location,
    private router: Router,
    private cartService: CartService,
    private cookieService: CookieService,
    private loginService: LoginService
  ) {}

  ngOnDestroy(){
    //this.cartService.carrinho.unsubscribe();
  }

  ngAfterContentInit(){
    let pedido = this.cartService.carrinho.getValue();

    if(pedido?.PedidoItem?.length == 0){
      let cart = localStorage.getItem('cart');

      if(cart){
        this.cartService.carrinho.next(JSON.parse(cart));
      }
    }

  }

  ngOnInit() {
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
      if (event instanceof NavigationStart) {
        if (event.url != this.lastPoppedUrl)
          this.yScrollStack.push(window.scrollY);
      } else if (event instanceof NavigationEnd) {
        if (event.url == this.lastPoppedUrl) {
          this.lastPoppedUrl = '';
          window.scrollTo(0, this.yScrollStack.pop()!);
        } else window.scrollTo(0, 0);
      }
    });
    this.location.subscribe((ev: PopStateEvent) => {
      this.lastPoppedUrl = ev.url!;
    });

    this.cartService.carrinho?.subscribe((success) => {               
      this.cartItems = success.PedidoItem?.length || 0;                  
    });    

    this.loginService.isUserLogged.subscribe(success => {
      this.isUserLogged = success;
    });
  }

  isHome() {
    var titlee = this.location.prepareExternalUrl(this.location.path());

    if (titlee === '#/home') {
      return true;
    } else {
      return false;
    }
  }
  isDocumentation() {
    var titlee = this.location.prepareExternalUrl(this.location.path());
    if (titlee === '#/documentation') {
      return true;
    } else {
      return false;
    }
  }
}
