import { Categoria } from "./Categoria"
import { Condicao } from "./Condicao"
import { Manga } from "./Manga"
import { Marca } from "./Marca"
import { Modelagem } from "./Modelagem"
import { ProdutoImagem } from "./ProdutoImagem"
import { Tamanho } from "./Tamanho"
import { Tecido } from "./Tecido"

export class Produto {
    Id!: string
    Nome!: string
    Descricao: string | undefined    
    Slug!: string
    ImagemPrincipal!: string
    MarcaId: number | undefined
    CategoriaId: number | undefined
    ValorCompra!: number
    ValorVenda!: number
    ValorPromocional: number | undefined
    Estoque!: number
    Ativo!: boolean
    DataCriacao!: Date
    TamanhoId!: number
    CondicaoId!: number
    MangaId: number | undefined
    ModelagemId: number | undefined
    TecidoId: number | undefined
    Cor!: string
    Medidas!: string
    Observacoes: string | undefined
    Categoria: Categoria | undefined
    Condicao!: Condicao        
    Manga: Manga | undefined
    Marca: Marca | undefined
    Modelagem: Modelagem | undefined
    Tamanho!: Tamanho
    Tecido: Tecido | undefined
    ProdutoImagem!: ProdutoImagem[] | undefined
}