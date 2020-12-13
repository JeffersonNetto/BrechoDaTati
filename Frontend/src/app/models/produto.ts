export class Produto {
    Id!: string
    Nome!: string
    Descricao: string | undefined
    MarcaId: number | undefined
    CategoriaId: number | undefined
    ValorCompra!: number
    ValorVenda!: number
    Estoque!: number
    Ativo: boolean = true
    DataCriacao!: Date
    DataAtualizacao: Date | undefined
    TamanhoId!: number
    CondicaoId!: number
    MangaId: number | undefined
    ModelagemId: number | undefined
    TecidoId: number | undefined
    Cor!: string
    Medidas!: string
    Observacoes: string | undefined
    // Categoria: Categoria        
    // Condicao: Condicao        
    // Manga: Manga        
    // Marca: Marca        
    // Modelagem: Modelagem        
    // Tamanho: Tamanho        
    // Tecido: Tecido
}