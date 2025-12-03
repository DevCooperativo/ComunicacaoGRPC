import * as grpc from "@grpc/grpc-js";
import { competicaoProto, integrantesProto } from "../grpc/Clients";
import { Request, Response } from "express";
import { promisify } from "util"

export class IntegrantesController {
    _integrantesClient: grpc.GrpcObject
    constructor() {
        this._integrantesClient = new integrantesProto.Comunicacao.Grpc.IntegrantesService("localhost:5183", grpc.credentials.createInsecure())

    }

    async Listar(req: Request, res: Response) {
        try {
            const listarIntegrantesAsync = promisify(this._integrantesClient.Listar).bind(this._integrantesClient)
            let integrantesResult = await listarIntegrantesAsync({})
            return res.status(200).json({ ...integrantesResult.integrantes });
        } catch (err) {
            return res.status(500).json({ message: err })
        }
    }



}