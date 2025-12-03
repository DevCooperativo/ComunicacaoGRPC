import express from "express"
import { routes } from "./routes"
import cors from "cors"
const app = express()

app.use(express.json())
app.use(cors())
app.use(function (req, res, next) {
    res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
    res.header("Access-Control-Allow-Headers", "X-Requested-With");
    res.header("Access-Control-Allow-Headers", "Access-Control-Allow-Origin");
    next();
})
app.use(routes)


app.listen((5000), "0.0.0.0", () => {
    console.log("Porta 5000")
})