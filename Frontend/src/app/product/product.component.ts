import { Component, OnInit } from '@angular/core';
import { Produto } from '../models/Produto';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  produto = {
    Id: '123456',
    Nome: 'Produto01',
    Descricao: undefined,
    Slug: 'produto01',
    ValorCompra: 100,
    ValorVenda: 199.90,
    ValorPromocional: 149.90,
    Estoque: 10,
    Ativo: true,
    DataCriacao: new Date(),
    CondicaoId: 1,
    Cor: 'Azul',        
    Medidas: 'Medidas aqui testando algum texto que seja razoavelmente grande',
    TamanhoId: 1,
    Tamanho: {
      Descricao: 'G'
    },
    Marca: {
      Nome: 'Lezalez'
    },
    Condicao: {
      Descricao: 'Usado'
    }
  };

  constructor() { }

  ngOnInit(): void {
  }

}
