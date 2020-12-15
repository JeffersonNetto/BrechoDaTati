import { Component, OnInit } from '@angular/core';
//import { Produto } from '../models/Produto';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent implements OnInit {

  produtos = [
    {
      Id: '123456789',
      Nome: 'Produto xxx',
      Descricao: 'descrição qualquer',
      CategoriaId: 1,
      CondicaoId: 1,
      DataCriacao: new Date(),
      Estoque: 10,
      Medidas: 'medidas bla bla bla',
      MarcaId: 1,
      ValorCompra: 100,
      ValorVenda: 200,
      Cor: 'Azul',
      MangaId: 1,
      TamanhoId: 1,
      ModelagemId:1,
      Ativo: true,
      TecidoId: 1,      
      Observacoes: 'teste teste'            
    },
    {
      Id: '123456789',
      Nome: 'Pruduto yyy',
      Descricao: 'descrição qualquer',
      CategoriaId: 1,
      CondicaoId: 1,
      DataCriacao: new Date(),
      Estoque: 10,
      Medidas: 'medidas bla bla bla',
      MarcaId: 1,
      ValorCompra: 100,
      ValorVenda: 200,
      Cor: 'Azul',
      MangaId: 1,
      TamanhoId: 1,
      ModelagemId:1,
      Ativo: true,
      TecidoId: 1,      
      Observacoes: 'teste teste'            
    },
    {
      Id: '123456789',
      Nome: 'Pruduto zzz',
      Descricao: 'descrição qualquer',
      CategoriaId: 1,
      CondicaoId: 1,
      DataCriacao: new Date(),
      Estoque: 10,
      Medidas: 'medidas bla bla bla',
      MarcaId: 1,
      ValorCompra: 100,
      ValorVenda: 200,
      Cor: 'Azul',
      MangaId: 1,
      TamanhoId: 1,
      ModelagemId:1,
      Ativo: true,
      TecidoId: 1,      
      Observacoes: 'teste teste'            
    },
    {
      Id: '123456789',
      Nome: 'Pruduto xxx',
      Descricao: 'descrição qualquer',
      CategoriaId: 1,
      CondicaoId: 1,
      DataCriacao: new Date(),
      Estoque: 10,
      Medidas: 'medidas bla bla bla',
      MarcaId: 1,
      ValorCompra: 100,
      ValorVenda: 200,
      Cor: 'Azul',
      MangaId: 1,
      TamanhoId: 1,
      ModelagemId:1,
      Ativo: true,
      TecidoId: 1,      
      Observacoes: 'teste teste'            
    },
    {
      Id: '123456789',
      Nome: 'Pruduto xxx',
      Descricao: 'descrição qualquer',
      CategoriaId: 1,
      CondicaoId: 1,
      DataCriacao: new Date(),
      Estoque: 10,
      Medidas: 'medidas bla bla bla',
      MarcaId: 1,
      ValorCompra: 100,
      ValorVenda: 200,
      Cor: 'Azul',
      MangaId: 1,
      TamanhoId: 1,
      ModelagemId:1,
      Ativo: true,
      TecidoId: 1,      
      Observacoes: 'teste teste'            
    },
  ];


  constructor() { }

  ngOnInit(): void {

  }

}
