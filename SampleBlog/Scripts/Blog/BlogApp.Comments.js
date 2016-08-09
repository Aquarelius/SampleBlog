var commentsApp = angular.module('BlogApp.Comments', []);
commentsApp.controller('commentsController', function ($scope, $http) {
    $scope.comments = [];
    $scope.parentCommentId = null;
    $scope.init = function (postId, userId, isAdmin) {
        $scope.postId = postId;
        $scope.userId = userId;
        $scope.isAdmin = isAdmin;
        $scope.message = "";
        $http({
            method: 'GET',
            url: '/api/post/' + $scope.postId + "/comments"
        }).then(function successCallback(response) {
            $scope.comments = [];
            //Sort comments
            for (var i in response.data) {
                var comm = response.data[i];
                if (comm.ParentCommentId == null) {
                    comm.Comments = [];
                    tools.findChildComments(comm, response.data);
                    $scope.comments.push(comm);
                }
            }
        });
    };
    $scope.write = function (message) {
        var parentCommentId = null;
        if ($scope.parentComment != null) parentCommentId = $scope.parentComment.Id;
        $http({
            method: 'POST',
            url: '/api/comments',
            data: {
                PostId: $scope.postId,
                Text: message,
                ParentCommentId: parentCommentId
            }
        }).then(function (response) {
            response.data.Comments = [];
            if ($scope.parentComment == null) {
                $scope.comments.push(response.data);
            } else {
                $scope.parentComment.Comments.push(response.data);
            }
            $scope.message = "";
            $scope.parentComment = null;
        });
    };

    $scope.delete = function (id) {
        $http({
            method: 'DELETE',
            url: '/api/comments/' + id
        }).then(function (response) {
            for (var i in response.data) {
                tools.deleteInChilds(response.data[i], $scope.comments);
            }
        });
    }

    $scope.reply = function (comment) {
        $scope.parentComment = comment;
    }
});

var tools = {
    findChildComments: function (parent, comments) {
        for (var i in comments) {
            if (comments[i].ParentCommentId == parent.Id) {
                comments[i].Comments = [];
                tools.findChildComments(comments[i], comments);
                parent.Comments.push(comments[i]);
            }
        }
    },
    findComment: function (id, collection) {
        for (var i in collection) {
            if (collection[i].Id == id) return collection[i];
            if (collection[i].Comments.length > 0) {
                var comm = tools.findComment(id, collection[i].Comments);
                if (comm != null) return comm;
            }
        }
        return null;
    },
    deleteInChilds: function(id, collection) {
        for (var i in collection) {
            if (collection[i].Id == id) {
                collection.splice(i, 1);
            } else {
                tools.deleteInChilds(id, collection[i].Comments);
            }
        }
    }
}