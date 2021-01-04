import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.scss']
})
export class CartComponent implements OnInit {

  items: any[] = [
    {
      Titulo: 'Teste01',
      Descricao: 'Descrição 01',
      Valor: 'R$ 149,99',
      Quantidade: 1
    },
    {
      Titulo: 'Teste02',
      Descricao: 'Descrição 02',
      Valor: 'R$ 199,99',
      Quantidade: 3
    },
    {
      Titulo: 'Teste03',
      Descricao: 'Descrição 03',
      Valor: 'R$ 99,99',
      Quantidade: 2
    },
  ]

  constructor() { }

  ngOnInit(): void {
  }

  Diminuir(item: any) {    
    let i = this.items.findIndex(_ => _.Titulo == item.Titulo)

    if(this.items[i].Quantidade > 1) {
      this.items[i].Quantidade -= 1;    
    }    
  }

  Aumentar(item: any) {    
    let i = this.items.findIndex(_ => _.Titulo == item.Titulo)
    this.items[i].Quantidade += 1;    
  }

  Remover(item: any){
    let i = this.items.findIndex(_ => _.Titulo == item.Titulo)
    this.items.splice(i, 1);
  }

}
