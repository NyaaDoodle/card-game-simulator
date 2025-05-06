const app = require('express')()
const { readFile } = require('fs')
const PORT = 8080

app.listen(PORT, () => console.log(`Running on port ${PORT}`))

app.get('/templates/:id', (req, res) => {
    const { id } = req.params
    if (id == 1 || id == 2) {
        readFile(`./fake_database/${id}.json`, 'utf-8', (err, contents) => {
            if (err) {
                res.status(500).send(`Server error while reading file ${id}.json`)
            }
            res.status(200).send(contents)
        })
    }
    else {
        res.status(404).send('File not found')
    }
})