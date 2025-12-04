import * as grpc from "@grpc/grpc-js";
import { competicaoProto, integrantesProto } from "../grpc/Clients";
import { Request, Response } from "express";
import { promisify } from "util"
import { Timestamp } from 'google-protobuf/google/protobuf/timestamp_pb';
import { timeStamp } from "console";

export class CompeticaoController {
    _competicaoClient: grpc.GrpcObject
    _integrantesClient: grpc.GrpcObject
    constructor() {
        this._competicaoClient = new competicaoProto.Comunicacao.Grpc.CompeticaoService("localhost:5183", grpc.credentials.createInsecure())
        this._integrantesClient = new integrantesProto.Comunicacao.Grpc.IntegrantesService("localhost:5183", grpc.credentials.createInsecure())

    }

    async Listar(req: Request, res: Response) {
        try {
            const listarCompeticaoAsync = promisify(this._competicaoClient.Listar).bind(this._competicaoClient)
            let competicaoResult = await listarCompeticaoAsync({})
            return res.status(200).json({ ...competicaoResult.competicoes });
        } catch (err) {
            return res.status(500).json({ message: err })
        }
    }


    async Inserir(req: Request, res: Response) {
        try {
            const { data, nome } = req.body;
            if (!data)
                return res.status(400).json({ message: "Data da competição é obrigatória" })


            const [year, month, day] = data.split("-").map(Number);

            const jsDate = new Date(Date.UTC(year, month - 1, day));
            if (jsDate.getTime() < (new Date().getTime()))
                return res.status(400).json({ message: "Registre competições que ocorrerão só a partir do dia seguinte" })
            if (Number((String(data)).split("-")[0]) > 9999)
                return res.status(403).json({ message: "A gente vai morrer antes..." })
            if (!nome)
                return res.status(400).json({ message: "Nome da competição é obrigatório" })

            console.log("JS DATE:", jsDate, "TIME:", jsDate.getTime());

            const seconds = (Math.floor(jsDate.getTime() / 1000));
            const nanos = ((jsDate.getTime() % 1000) * 1e6);

            const request = {
                nome,
                data: {
                    seconds,
                    nanos
                }
            };

            const competicaoResult = await new Promise((resolve, reject) => {
                this._competicaoClient.Inserir(request, (err, response) => {
                    if (err) return reject(err);
                    resolve(response);
                });
            });

            return res.status(200).json(competicaoResult);

        } catch (err) {
            console.log(err);
            return res.status(500).json({ error: err.message ?? err });
        }
    }

}