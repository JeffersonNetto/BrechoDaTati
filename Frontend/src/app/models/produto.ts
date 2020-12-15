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
    Descricao: string | undefined = undefined    
    Slug!: string
    MarcaId: number | undefined = undefined
    CategoriaId: number | undefined = undefined
    ValorCompra!: number
    ValorVenda!: number
    ValorPromocional: number | undefined = undefined
    Estoque!: number
    Ativo!: boolean
    DataCriacao!: Date
    TamanhoId!: number
    CondicaoId!: number
    MangaId: number | undefined = undefined
    ModelagemId: number | undefined = undefined
    TecidoId: number | undefined = undefined
    Cor!: string
    Medidas!: string
    Observacoes: string | undefined = undefined
    Categoria: Categoria | undefined  = undefined
    Condicao!: Condicao        
    Manga: Manga | undefined  = undefined
    Marca: Marca | undefined = undefined
    Modelagem: Modelagem | undefined = undefined
    Tamanho!: Tamanho
    Tecido: Tecido | undefined = undefined
    ProdutoImagem!: ProdutoImagem[]
}