import { Router } from "express";
import { ApostasController } from "../controllers/ApostasController";

const routes = Router()

const apostaController = new ApostasController()


routes.get("/apostas/", (req, res) => apostaController.Listar(req, res))
routes.post("/apostas/", (req, res) => apostaController.Inserir(req, res))
export { routes }