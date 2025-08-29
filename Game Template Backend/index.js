const express = require('express');
const cors = require('cors');
const fs = require('fs');
const path = require('path');

const SERVER_PORT = 80;
const TEMPLATES_DIR = path.join(__dirname, 'templates');
const IMAGES_DIR = path.join(__dirname, 'images');
const THUMBNAILS_DIR = path.join(__dirname, 'thumbnails');

const app = express();

// Enabling CORS
app.use(cors());

// Images and thumbnails serving
app.use('/images', express.static(IMAGES_DIR));
app.use('/thumbnails', express.static(THUMBNAILS_DIR));

const TEMPLATE_JSON_FILENAME = 'template.json';

function getTemplateIds() {
    return fs.readdirSync(TEMPLATES_DIR, {withFileTypes: true})
            .filter(dirent => dirent.isDirectory())
            .map(dirent => dirent.name);
}

function getAvailableTemplates(templateIds) {
    const availableTemplates = [];
    templateIds.forEach(templateId => {
        const templateJsonPath = path.join(TEMPLATES_DIR, templateId, TEMPLATE_JSON_FILENAME);
        console.log(`getAvailableTemplates: Attempting to look at path: ${templateJsonPath}`)
        if (fs.existsSync(templateJsonPath)) {
            availableTemplates.push(templateId);
        }
    })
    return availableTemplates;
}

function printBaseDirectories() {
    console.log(`Templates directory: ${TEMPLATES_DIR}`);
    console.log(`Images directory: ${IMAGES_DIR}`);
    console.log(`Thumbnails directory: ${TEMPLATES_DIR}`);
}

// Endpoints

// Get available game templates
app.get('/templates', (req, res) => {
    console.log('Received request for /templates endpoint');
    try {
        const templateIds = getTemplateIds();
        const availableTemplates = getAvailableTemplates(templateIds);
        console.log(`Found ${availableTemplates.length} available game templates`);
        res.json(availableTemplates);
    }
    catch (error) {
        console.error('Error fetching game templates:', error);
        res.status(500).send('Server error while attempting to fetch game templates');
    }
});

// Get game template JSON file
app.get('/templates/:id', (req, res) => {
    const id = req.params.id;
    console.log(`Received request for game template ${id}`);
    try {
        const filePath = path.join(TEMPLATES_DIR, id, TEMPLATE_JSON_FILENAME);
        console.log(`templates/:id endpoint: Attempting to look at path: ${filePath}`);
        if (fs.existsSync(filePath)) {
            res.sendFile(filePath);
        }
        else {
            console.warn(`Game template JSON file for game template ${id} was not found`);
            res.status(404).send(`Game template with ID ${id} was not found`);
        }
    }
    catch (error) {
        console.error(`Error fetching game template ${id} JSON file:`, error);
        res.status(500).send(`Server error while attempting to fetch game template data with ID: ${id}`);
    }
})

app.listen(SERVER_PORT, () => {
    console.log(`Card Game Simulator game template server running on port ${SERVER_PORT}`);
    printBaseDirectories();
});