﻿function MoodsCloud(containerSelector, moods) {
    var WIDTH = 1200;
    var HEIGHT = 400;

    this.update = function () {
        var words = getWords();
        doLayout(words);
    }

    function getWords() {
        return moods.map(moodToWord);
    }

    function moodToWord(mood) {
        return { text: mood.value, size: mood.weight };
    }

    function doLayout(words) {
        d3.layout.cloud()
            .size([WIDTH, HEIGHT])
            .words(words)
            .padding(8)
            .rotate(getRotation)
            .font("Impact")
            .fontSize(getFontSize)
            .on("end", onLayoutDone)
            .start();

        function getRotation() {
            return ~~(Math.random() * 2) * 90;
        }

        function getFontSize(word) {
            return 20 + word.size * 250;
        }
    }

    function onLayoutDone(data) {
        var fillScale = d3.scale.category20();

        $(containerSelector).empty();

        d3.select(containerSelector)
            .append("svg")
            .attr("width", WIDTH)
            .attr("height", HEIGHT)
            .append("g")
            .attr("transform", getGroupTransform)
            .selectAll("text")
            .data(data)
            .enter()
            .append("text")
            .attr("transform", getTransform)
            .attr("text-anchor", "middle")
            .style("font-size", getFontSize)
            .style("fill", getFill)
            .text(getText);

        /*
         * The whole svg group has to be translated with 50%, 
         * as word (item) coordinates are relative to the center of the cloud etc.
         */
        function getGroupTransform() {
            return "translate(" + [WIDTH / 2, HEIGHT / 2] + ")";
        }

        function getTransform(item) {
            return "translate(" + [item.x, item.y] + ")rotate(" + item.rotate + ")";
        }

        function getFontSize(item) {
            return item.size + "px";
        }

        function getFill(item, index) {
            return fillScale(index);
        }

        function getText(item) {
            return item.text;
        }
    }
};