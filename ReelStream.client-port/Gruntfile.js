//Concatinate all third party libraries into one file.

module.exports = function (grunt) {

  // Project configuration.
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),

      
    concat: {
        options: {
          separator: ';',
        },
        dist: {
            src: ['./src/assets/libs/flow/*.min.js'],
            dest: './src/assets/libs/workspace/libraries.js'
        }
    },

    uglify: {
      build: {
          src: './src/assets/libs/workspace/libraries.js',
          dest: './src/assets/libs/workspace/libraries.min.js'
      }
    }
  });
    
  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-uglify');

  // Default task(s).
  grunt.registerTask('default', ['concat', 'uglify']);

};