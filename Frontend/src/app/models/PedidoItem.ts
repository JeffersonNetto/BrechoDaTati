import { Produto } from "./Produto"

export class PedidoItem {
    PedidoId!: string
    ProdutoId!: string
    ValorUnitario!: number
    ValorUnitarioPago!: number
    Quantidade!: number
    Produto!: Produto
}